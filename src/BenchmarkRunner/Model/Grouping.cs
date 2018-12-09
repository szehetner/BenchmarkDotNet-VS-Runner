using System;

namespace BenchmarkRunner.Model
{
    public enum Grouping
    {
        ProjectClass,
        ProjectNamespaceClass,
        ProjectCategoryClass,
    }

    public static class GroupName
    {
        public const string PROJECT_CLASS = "Class";
        public const string PROJECT_NAMESPACE_CLASS = "Namespace, Class";
        public const string PROJECT_CATEGORY_CLASS = "Category, Class";

        public static string[] AllNames => new []
                                            {
                                                PROJECT_CLASS,
                                                PROJECT_NAMESPACE_CLASS,
                                                PROJECT_CATEGORY_CLASS
                                            };

        public static Grouping GetValue(string name)
        {
            switch (name)
            {
                case PROJECT_CLASS:
                    return Grouping.ProjectClass;
                case PROJECT_NAMESPACE_CLASS:
                    return Grouping.ProjectNamespaceClass;
                case PROJECT_CATEGORY_CLASS:
                    return Grouping.ProjectCategoryClass;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
