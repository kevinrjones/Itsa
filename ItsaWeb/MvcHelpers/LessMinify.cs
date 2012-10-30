using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Optimization;
using dotless.Core;
using dotless.Core.Abstractions;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Loggers;
using dotless.Core.Parser;
 
namespace ItsaWeb.MvcHelpers
{
    public class LessMinify : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse bundle)
        {
            if (bundle == null)
            {
                throw new ArgumentNullException("bundle");
            }

            context.HttpContext.Response.Cache.SetLastModifiedFromFileDependencies();

            var lessParser = new Parser();
            ILessEngine lessEngine = CreateLessEngine(lessParser);

            var content = new StringBuilder(bundle.Content.Length);

            foreach (FileInfo file in bundle.Files)
            {
                SetCurrentFilePath(lessParser, file.FullName);
                string source = File.ReadAllText(file.FullName);
                content.Append(lessEngine.TransformToCss(source, file.FullName));
                content.AppendLine();

                AddFileDependencies(lessParser);
            }

            bundle.ContentType = "text/css";
            bundle.Content = content.ToString();
        }

        /// 
        /// Creates an instance of LESS engine.
        /// 
        /// The LESS parser.
        private ILessEngine CreateLessEngine(Parser lessParser)
        {
            var logger = new AspNetTraceLogger(LogLevel.Debug, new Http());
            return new LessEngine(lessParser, logger, true, false);
        }

        /// 
        /// Adds imported files to the collection of files on which the current response is dependent.
        /// 
        /// The LESS parser.
        private void AddFileDependencies(Parser lessParser)
        {
            IPathResolver pathResolver = GetPathResolver(lessParser);

            foreach (string importedFilePath in lessParser.Importer.Imports)
            {
                string fullPath = pathResolver.GetFullPath(importedFilePath);
                HttpContext.Current.Response.AddFileDependency(fullPath);
            }

            lessParser.Importer.Imports.Clear();
        }

        /// 
        /// Returns an  instance used by the specified LESS lessParser.
        /// 
        /// The LESS prser.
        private IPathResolver GetPathResolver(Parser lessParser)
        {
            var importer = lessParser.Importer as Importer;
            if (importer != null)
            {
                var fileReader = importer.FileReader as FileReader;
                if (fileReader != null)
                {
                    return fileReader.PathResolver;
                }
            }

            return null;
        }

        /// 
        /// Informs the LESS parser about the path to the currently processed file. 
        /// This is done by using custom  implementation.
        /// 
        /// The LESS parser.
        /// The path to the currently processed file.
        private void SetCurrentFilePath(Parser lessParser, string currentFilePath)
        {
            var importer = lessParser.Importer as Importer;
            if (importer != null)
            {
                var fileReader = importer.FileReader as FileReader;

                if (fileReader == null)
                {
                    importer.FileReader = fileReader = new FileReader();
                }

                var pathResolver = fileReader.PathResolver as ImportedFilePathResolver;

                if (pathResolver != null)
                {
                    pathResolver.CurrentFilePath = currentFilePath;
                }
                else
                {
                    fileReader.PathResolver = new ImportedFilePathResolver(currentFilePath);
                }
            }
            else
            {
                throw new InvalidOperationException("Unexpected importer type on dotless parser");
            }
        }
    }
}