CREATE TABLE [ICollectionUser] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [ASPNetIdentityID] nvarchar(450),
  [first_name] nvarchar(50) NOT NULL,
  [last_name] nvarchar(50) NOT NULL,
  [user_name] nvarchar(50) NOT NULL,
  [date_joined] DateTime,
  [about_me] nvarchar(250),
  [profile_pic_id] int
)
GO

CREATE TABLE [Follow] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [follower] int,
  [followed] int,
  [began] DateTime
)
GO

CREATE TABLE [Collection] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(100) NOT NULL,
  [visibility] int NOT NULL,
  [user_id] int,
  [date_made] DateTime,
  [route] nvarchar(100) NOT NULL,
)
GO

CREATE TABLE [Keyword] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(50) NOT NULL
)
GO

CREATE TABLE [Photo] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(50),
  [data] varbinary(MAX),
  [user_id] int,
  [date_uploaded] DateTime,
  [PhotoGUID] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID()
)
GO

CREATE TABLE [FriendsWith] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [user1_id] int,
  [user2_id] int,
  [began] DateTime
)
GO

CREATE TABLE [CollectionPhoto] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [collect_id] int,
  [photo_id] int,
  [photo_rank] int,
  [title] nvarchar(50),
  [Description] nvarchar(50),
  [date_added] DateTime
)
GO

CREATE TABLE [CollectionKeyword] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [collect_id] int,
  [keyword_id] int,
  [date_added] DateTime
)
GO



ALTER TABLE [Follow] ADD CONSTRAINT [Follow_fk_ICollectionUser_One] FOREIGN KEY ([follower]) REFERENCES [ICollectionUser] ([id])
GO

ALTER TABLE [Follow] ADD CONSTRAINT [Follow_fk_ICollectionUser_Two] FOREIGN KEY ([followed]) REFERENCES [ICollectionUser] ([id])
GO

ALTER TABLE [Collection] ADD CONSTRAINT [Collection_fk_ICollectionUser] FOREIGN KEY ([user_id]) REFERENCES [ICollectionUser] ([id])
GO

ALTER TABLE [Photo] ADD CONSTRAINT [Photo_fk_ICollectionUser] FOREIGN KEY ([user_id]) REFERENCES [ICollectionUser] ([id])
GO

ALTER TABLE [FriendsWith] ADD CONSTRAINT [FriendsWith_fk_ICollectionUser_One] FOREIGN KEY ([user1_id]) REFERENCES [ICollectionUser] ([id])
GO

ALTER TABLE [FriendsWith] ADD CONSTRAINT [FriendsWith_fk_ICollectionUser_Two] FOREIGN KEY ([user2_id]) REFERENCES [ICollectionUser] ([id])
GO

ALTER TABLE [CollectionPhoto] ADD CONSTRAINT [CollectionPhoto_fk_Collection] FOREIGN KEY ([collect_id]) REFERENCES [Collection] ([id])
GO

ALTER TABLE [CollectionPhoto] ADD CONSTRAINT [CollectionPhoto_fk_Photo] FOREIGN KEY ([photo_id]) REFERENCES [Photo] ([id])
GO

ALTER TABLE [CollectionKeyword] ADD CONSTRAINT [CollectionKeyword_fk_Collection] FOREIGN KEY ([collect_id]) REFERENCES [Collection] ([id])
GO

ALTER TABLE [CollectionKeyword] ADD CONSTRAINT [CollectionKeyword_fk_Keyword] FOREIGN KEY ([keyword_id]) REFERENCES [Keyword] ([id])
GO