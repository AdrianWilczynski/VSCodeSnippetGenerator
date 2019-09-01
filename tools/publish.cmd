pushd ..\src\VSCodeSnippetGenerator.Web
libman restore
call tsc
dotnet publish -c Release
popd