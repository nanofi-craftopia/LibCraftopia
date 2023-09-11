const fs = require('fs');
const { spawnSync } = require('child_process');
const slash = require("@esm2cjs/slash").default;

var ideTypes = {
  Pro: 'Professional',
  Enterprise: 'Enterprise',
  Community: 'Community',
  BuildTools: 'BuildTools'
};

var programFilesDir = process.env['ProgramW6432'] || process.env.PROGRAMFILES;
var programFilesDirx86 = process.env['programfiles(x86)'] || process.env.PROGRAMFILES;

const msbuildPathFor17 = (version, subdir) => {
  var dir = process.env.vsInstallDir;
  if(dir === undefined) {
    var baseDir = programFilesDir + '/' + 'Microsoft Visual Studio/' + version + '/';
    if(fs.existsSync(baseDir + ideTypes.Pro))
      dir = baseDir + ideTypes.Pro + '/';
    else if(fs.existsSync(baseDir + ideTypes.Enterprise))
      dir = baseDir + ideTypes.Enterprise + '/';
    else if(fs.existsSync(baseDir + ideTypes.Community))
      dir = baseDir + ideTypes.Community + '/';
    else if(fs.existsSync(baseDir + ideTypes.BuildTools))
      dir = baseDir + ideTypes.BuildTools + '/';
  }
  if(dir === undefined) return undefined;
  var path = dir + 'MSBuild/' + subdir + '/bin/msbuild.exe';
  if(fs.existsSync(path)) return path;
  return undefined;
};

const msbuildPathFor15or16  = (version, subdir) => {
  var dir = process.env.vsInstallDir;
  if(dir === undefined) {
    var baseDir = programFilesDirx86 + '/' + 'Microsoft Visual Studio/' + version + '/';
    if(fs.existsSync(baseDir + ideTypes.Pro))
      dir = baseDir + ideTypes.Pro + '/';
    else if(fs.existsSync(baseDir + ideTypes.Enterprise))
      dir = baseDir + ideTypes.Enterprise + '/';
    else if(fs.existsSync(baseDir + ideTypes.Community))
      dir = baseDir + ideTypes.Community + '/';
    else if(fs.existsSync(baseDir + ideTypes.BuildTools))
      dir = baseDir + ideTypes.BuildTools + '/';
  }
  if(dir === undefined) return undefined;
  var path = dir + 'MSBuild/' + subdir + '/bin/msbuild.exe';
  if(fs.existsSync(path)) return path;
  return undefined;
};

const msbuildPathFor12and14 = () => {
  var versions = ["14.0", "12.0"];
  var baseDir = programFilesDirx86 + '/' + 'MSBuild';
  for (const ver of versions) {
    var dir = baseDir + '/' + ver + '/' + 'bin';
    var path = dir + '/' + 'amd64' + '/' + 'msbuild.exe';
    if(fs.existsSync(path)) return path;
    path = dir + '/' + 'msbuild.exe';
    if(fs.existsSync(path)) return path;
    return undefined;
  }
};

const msbuildPathOlder = () => {
  var versions = ['4.0.30319', '4.0.30319', '3.5', '3.0', '2.0.50727'];
  var baseDir =  process.env.WINDIR + '/' + 'microsoft.net';
  for (const ver of versions) {
    var path = baseDir + '/' + 'Framework64' + '/' + 'v' + ver + '/' + 'msbuild.exe';
    if(fs.existsSync(path)) return path;
    path = baseDir + '/' + 'Framework' + '/' + 'v' + ver + '/' + 'msbuild.exe';
    if(fs.existsSync(path)) return path;
  }
}

const searchMSBuildPath = () => {

  var path = undefined
  if(path == undefined)
    path = msbuildPathFor17("2022", "current");
  if(path === undefined)
    path = msbuildPathFor15or16("2019", "current");
  if(path === undefined)
    path = msbuildPathFor15or16("2017", "15.0");
  if(path === undefined) 
    path = msbuildPathFor12and14();
  if(path === undefined) 
    path = msbuildPathOlder();
  return path;
};

var msbuild = searchMSBuildPath();
if(msbuild === undefined) {
  console.error("MSBuild not found");
}

msbuild = slash(msbuild);

// const ps = spawnSync(msbuild, ['LibCraftopia.sln', '/t:rebuild', '/p:Configuration=Release']);
const ps = spawnSync("powershell.exe", ["-Command", `chcp 65001; & "${msbuild}" LibCraftopia.sln /t:rebuild /p:Configuration=Release`])
console.warn(ps.status);
console.warn(ps.stdout.toString("utf-8"));