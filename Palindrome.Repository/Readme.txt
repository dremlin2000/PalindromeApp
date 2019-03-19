The following commands should be executed in command prompt (cmd.exe) in Palindrome.Repository folder

How to add a new migration:
	dotnet ef migrations add InitialCreate --startup-project ..\Palindrome.Web

How to apply a new migration into database:
	dotnet ef database update --startup-project ..\Palindrome.Web
