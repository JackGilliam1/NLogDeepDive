using System;
using FubuCore;
using FubuMVC.Core;
using Serenity;

namespace TypeCalculator.StoryTeller
{
    public class TypeCalculatorSystem : FubuMvcSystem<TypeCalculatorApplication>
    {
        public TypeCalculatorSystem()
            : base(GetAppSettings<TypeCalculatorApplication>())
        {
        }

        private static ApplicationSettings GetAppSettings<T>()
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory
                .ParentDirectory()
                .ParentDirectory()
                .ParentDirectory()
                .AppendPath(new []
                {
                    typeof(T).Assembly.GetName().Name
                });
            return new ApplicationSettings
            {
                ApplicationSourceName = typeof(T).AssemblyQualifiedName,
                PhysicalPath = directory,
                Port = 5501
            };
        }
    }
}
