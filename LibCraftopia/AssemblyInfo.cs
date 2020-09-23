using System.Reflection;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: Guid("45ff02ed-0a43-4a6c-a3e7-beebe36635aa")]
[assembly: AssemblyVersion(ThisAssembly.Git.BaseVersion.Major + "." + ThisAssembly.Git.BaseVersion.Minor + "." + ThisAssembly.Git.BaseVersion.Patch + "." + ThisAssembly.Git.Commits)]
[assembly: AssemblyFileVersion(ThisAssembly.Git.SemVer.Major + "." + ThisAssembly.Git.SemVer.Minor + "." + ThisAssembly.Git.SemVer.Patch + "." + ThisAssembly.Git.Commits)]
[assembly: AssemblyInformationalVersion(
	ThisAssembly.Git.SemVer.Major + "." +
	ThisAssembly.Git.SemVer.Minor + "." +
	ThisAssembly.Git.SemVer.Patch + "." +
	ThisAssembly.Git.Commits + "-" +
	ThisAssembly.Git.Branch + "+" +
	ThisAssembly.Git.Commit)]