USE [JaridaApplication]
GO

INSERT INTO [dbo].[ApiAuthentication]
           ([PkAuth]
           ,[AuthKey]
           ,[SecretKey]
           ,[IsActive])
     VALUES
           (NEWID()
           ,'{key}'
           ,'{secret}'
           ,1)
GO