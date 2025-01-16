using System.Reflection;

namespace Store.API;

public class PresentationAssemblyReference
{
  internal static readonly Assembly Assembly = typeof(PresentationAssemblyReference).Assembly;
}