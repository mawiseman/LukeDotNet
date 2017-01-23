using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using LukeDotNetWPF.Helper;
using LukeDotNetWPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LukeDotNetWPF.ViewModels
{
    public class LuceneIndexVM : ITabItemVM, INotifyPropertyChanged
    {
        #region ITabItemVM

        public string TabName
        {
            get
            {
                string tabName = LukeIndex.IndexName;

                if (IsReadOnly)
                    tabName += " (r)";

                return tabName;
            }
        }

        public ICommand CloseCommand { get; set; }

        #endregion

        public bool IsReadOnly { get; set; }

        public LukeIndex LukeIndex { get; set; }

        private ResourceManager _resources = new ResourceManager
        (
            typeof(LuceneIndexVM).Namespace.Replace("ViewModels", "Resources") + ".Messages",
            Assembly.GetAssembly(typeof(LuceneIndexVM))
        );

        #region Overview Tab Properties

        public int NumberOfFields { get; set; }

        public int NumberOfDocuments { get; set; }

        public int NumberOfTerms { get; set; }

        public bool HasDeletions { get; set; }

        public string IndexVersion { get; set; }

        public DateTime LastModified { get; set; }

        #endregion

        #region Search Tab Properties

        public List<string> Analyzers { get; set; }
        public string SelectedAnalyzer { get; set; }

        public List<string> Fields { get; set; }
        public string SelectedField { get; set; }

        private string _searchExpression = string.Empty;
        public string SearchExpression {
            get
            {
                return _searchExpression;
            }
            set
            {
                if (_searchExpression.Equals(value)) return;
                _searchExpression = value;

                QueryParser qp = CreateQueryParser();

                if (string.IsNullOrEmpty(SearchExpression))
                    ParsedQuery = _resources.GetString("EmptyQuery");

                try
                {
                    ParsedQuery = qp.Parse(SearchExpression).ToString();
                }
                catch (Exception ex)
                {
                    ParsedQuery = ex.Message;
                }

                OnPropertyChanged("SearchExpression");
                OnPropertyChanged("ParsedQuery");
            }
        }

        public string ParsedQuery { get; set; }

        //public string ParsedQuery
        //{
        //    get
        //    {
        //        QueryParser qp = CreateQueryParser();

        //        if (string.IsNullOrEmpty(SearchExpression))
        //            return _resources.GetString("EmptyQuery");

        //        try
        //        {
        //            return qp.Parse(SearchExpression).ToString();
        //        }
        //        catch (Exception ex)
        //        {
        //            return ex.Message;
        //        }
        //    }
        //}

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public LuceneIndexVM(string indexPath)
        {
            this.LukeIndex = new LukeIndex(indexPath);

            var indexDirectory = FSDirectory.Open(LukeIndex.IndexPath);
            var indexReader = IndexReader.Open(indexDirectory, true);

            Fields = indexReader
                .GetFieldNames(IndexReader.FieldOption.ALL)
                .OrderBy(f => f)
                .ToList();

            #region Overview Tab

            NumberOfFields = Fields.Count();

            NumberOfDocuments = indexReader.NumDocs();

            IndexVersion = IndexReader.GetCurrentVersion(indexDirectory).ToString();

            HasDeletions = indexReader.HasDeletions;

            NumberOfTerms = 0;
            TermEnum termsEnum = indexReader.Terms();
            while (termsEnum.Next()) NumberOfTerms++;
            termsEnum.Dispose();

            var lastModified = IndexReader.LastModified(indexDirectory);
            LastModified = new DateTime(lastModified * TimeSpan.TicksPerMillisecond).AddYears(1969).ToLocalTime();

            #endregion

            #region Search Tab

            Analyzers = ClassFinder
                .GetInstantiableSubtypes(typeof(Lucene.Net.Analysis.Analyzer))
                .Select(a => a.FullName)
                .ToList();

            OnPropertyChanged("Analyzers");

            #endregion
        }

        #region Search Tab

        //private ICommand UpdateParsedQueryView(ITabItemVM tabitemVM)
        //{
        //    return new SimpleCommand
        //    {
        //        CanExecuteDelegate = x => true,
        //        ExecuteDelegate = x => { this.DoUpdateParsedQueryView(tabitemVM); }
        //    };
        //}

        //protected virtual void DoUpdateParsedQueryView(ITabItemVM tabitemVM)
        //{
        //    QueryParser qp = CreateQueryParser();
            
        //    if (string.IsNullOrEmpty(SearchExpression))
        //    {
        //        //textParsed.Enabled = false;
        //        ParsedQuery = _Resources.GetString("EmptyQuery");
        //        return;
        //    }
        //    //textParsed.Enabled = true;

        //    try
        //    {
        //        ParsedQuery = qp.Parse(SearchExpression).ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        ParsedQuery = ex.Message;
        //    }
        //}

        #endregion

        private QueryParser CreateQueryParser()
        {
            if (string.IsNullOrEmpty(SelectedAnalyzer))
                SelectedAnalyzer = Analyzers.FirstOrDefault(a => a.Contains("StandardAnalyzer"));

            if (string.IsNullOrEmpty(SelectedAnalyzer))
                SelectedAnalyzer = Analyzers.FirstOrDefault();

            var analyzer = AnalyzerForName(SelectedAnalyzer);
            if (null == analyzer)
            {
                string s = "";
            }

            if (string.IsNullOrEmpty(SelectedField))
                SelectedField = Fields.FirstOrDefault();

            return new QueryParser(Lucene.Net.Util.Version.LUCENE_CURRENT, SelectedField, analyzer);
        }

        private Analyzer AnalyzerForName(string analyzerName)
        {
            try
            {
                // Trying to create type from Lucene.Net assembly
                Assembly a = Assembly.GetAssembly(typeof(Lucene.Net.Analysis.Analyzer));
                Type analyzerType = a.GetType(analyzerName);

                //TODO some of these now require default constructor values

                //var aa = new Lucene.Net.Analysis.KeywordAnalyzer();
                //var b = new Lucene.Net.Analysis.PerFieldAnalyzerWrapper(defaultAnalyzer);
                //var c = new Lucene.Net.Analysis.SimpleAnalyzer();
                //var d = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_CURRENT);
                //var e = new Lucene.Net.Analysis.StopAnalyzer(Lucene.Net.Util.Version.LUCENE_CURRENT);
                //var f = new Lucene.Net.Analysis.WhitespaceAnalyzer();

                // Trying to create with default constructor
                return (Analyzer)Activator.CreateInstance(analyzerType, Lucene.Net.Util.Version.LUCENE_CURRENT);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
