This is an example of using Syncfusions grid control to export an Excel file in Bolero, https://fsbolero.io/.

This is to demonstrate a bug in Syncfusions grid control that happens when the model has changed after a dispatch.
To reproduce:
1. Start Bolero: `# dotnet run -p src/FSharp.Bolero.Server`
2. Navigate to http://localhost:5000
3. Grid contains 3 rows
4. Press 'Excel Export'
5. File is successfully exported
6. Model has changed and grid now contains 1 row
7. Press 'Excel Export'
8. Nothing happens...

It seems that the 'OnClick' event isn't triggered - probably due to Blazors DOM rendering (But that is just a guess)
