pushd ..\src\VSCodeSnippetGenerator.Web
libman restore
call tsc
dotnet build
popd