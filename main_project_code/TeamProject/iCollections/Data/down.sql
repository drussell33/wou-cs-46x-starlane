ALTER TABLE [Follow] DROP CONSTRAINT [Follow_fk_ICollectionUser_One] 
ALTER TABLE [Follow] DROP CONSTRAINT [Follow_fk_ICollectionUser_Two] 
ALTER TABLE [Collection] DROP CONSTRAINT [Collection_fk_ICollectionUser] 
ALTER TABLE [Photo] DROP CONSTRAINT [Photo_fk_ICollectionUser]
ALTER TABLE [FriendsWith] DROP CONSTRAINT [FriendsWith_fk_ICollectionUser_One]
ALTER TABLE [FriendsWith] DROP CONSTRAINT [FriendsWith_fk_ICollectionUser_Two]
ALTER TABLE [CollectionPhoto] DROP CONSTRAINT [CollectionPhoto_fk_Collection] 
ALTER TABLE [CollectionPhoto] DROP CONSTRAINT [CollectionPhoto_fk_Photo] 
ALTER TABLE [CollectionKeyword] DROP CONSTRAINT [CollectionKeyword_fk_Collection] 
ALTER TABLE [CollectionKeyword] DROP CONSTRAINT [CollectionKeyword_fk_Keyword]


DROP TABLE [Follow];
DROP TABLE [Collection];
DROP TABLE [Keyword]; 
DROP TABLE [Photo];
DROP TABLE [FriendsWith];
DROP TABLE [CollectionPhoto];
DROP TABLE [CollectionKeyword];
DROP TABLE [ICollectionUser];
