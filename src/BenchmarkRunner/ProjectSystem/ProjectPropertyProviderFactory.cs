using EnvDTE;
using Microsoft.VisualStudio.ProjectSystem.Properties;

namespace BenchmarkRunner.ProjectSystem
{
    public class ProjectPropertyProviderFactory
    {
        public static IProjectPropertyProvider Create(Project project)
        {
            if (IsNewProjectFormat(project))
                return new CommonProjectPropertyProvider(project);

            if (project.ConfigurationManager?.ActiveConfiguration != null)
                return new LegacyProjectPropertyProvider(project);

            return null;
        }

        private static bool IsNewProjectFormat(Project vsProject)
        {
            return vsProject is IVsBrowseObjectContext;
        }
    }
}
