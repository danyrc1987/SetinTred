using System;
using System.Reflection;

namespace CloudEngineMX.Erp.SetinTred.Reporting.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}