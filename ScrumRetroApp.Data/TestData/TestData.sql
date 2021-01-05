Use ScrumRetroApp;
GO

-- TEAM INSERT
SET IDENTITY_INSERT [dbo].[Team] ON
INSERT INTO [dbo].[Team] ([Id], [Name]) VALUES (1, 'AHV-1')
INSERT INTO [dbo].[Team] ([Id], [Name]) VALUES (2, 'AHV-2')
INSERT INTO [dbo].[Team] ([Id], [Name]) VALUES (3, 'AHV-3')
INSERT INTO [dbo].[Team] ([Id], [Name]) VALUES (4, 'AHV-4')
SET IDENTITY_INSERT [dbo].Team OFF
Go

-- USER INSERT
SET IDENTITY_INSERT  [dbo].[User] ON
INSERT INTO [dbo].[User] ([Id], [TeamId], [Name], [Mail]) VALUES (1, 1, 'Yo-El', 'yo-el.tikue@m-s.ch')
INSERT INTO [dbo].[User] ([Id], [TeamId], [Name], [Mail]) VALUES (2, 1, 'Yo-El', 'yo-el.tikue@m-s.ch')
INSERT INTO [dbo].[User] ([Id], [TeamId], [Name], [Mail]) VALUES (3, 1, 'Yo-El', 'yo-el.tikue@m-s.ch')
INSERT INTO [dbo].[User] ([Id], [TeamId], [Name], [Mail]) VALUES (4, 1, 'Yo-El', 'yo-el.tikue@m-s.ch')
SET IDENTITY_INSERT  [dbo].[User] OFF
GO

-- DASHBOARD INSERT
SET IDENTITY_INSERT  [dbo].[Dashboard] ON
INSERT INTO [dbo].[Dashboard]([Id], [TeamId],[Title],[ActiveRetro]) VALUES (1, 1,'Dashboard1', 'Retro1')
INSERT INTO [dbo].[Dashboard]([Id], [TeamId],[Title],[ActiveRetro]) VALUES (2, 1,'Dashboard2', 'Retro1')
INSERT INTO [dbo].[Dashboard]([Id], [TeamId],[Title],[ActiveRetro]) VALUES (3, 1,'Dashboard3', 'Retro1')
INSERT INTO [dbo].[Dashboard]([Id], [TeamId],[Title],[ActiveRetro]) VALUES (4, 1,'Dashboard4', 'Retro1')
SET IDENTITY_INSERT  [dbo].[Dashboard] OFF
GO

-- EMOJI INSERT
SET IDENTITY_INSERT  [dbo].[Emoji] ON
INSERT INTO [dbo].[Emoji] ([Id], [EmojiPath], [Text], [Type], [Unicode]) VALUES (1, 'images\smile-regular.svg', 'Happy', 'Positive', 'U+1F600')
INSERT INTO [dbo].[Emoji] ([Id], [EmojiPath], [Text], [Type], [Unicode]) VALUES (2, 'images\smile-regular.svg', 'Okay', 'Positive', 'U+1F600')
INSERT INTO [dbo].[Emoji] ([Id], [EmojiPath], [Text], [Type], [Unicode]) VALUES (3, 'images\smile-regular.svg', 'Sad', 'Negative', 'U+1F600')
INSERT INTO [dbo].[Emoji] ([Id], [EmojiPath], [Text], [Type], [Unicode]) VALUES (4, 'images\smile-regular.svg', 'Angry', 'Negative', 'U+1F600')
SET IDENTITY_INSERT  [dbo].[Emoji] OFF
GO

--RETROCONFIGURATION INSERT
SET IDENTITY_INSERT  [dbo].[RetroConfiguration] ON
INSERT INTO [dbo].[RetroConfiguration] ([Id], [Anonymitiy], [MaxVotes]) VALUES (1, 0, 3)
SET IDENTITY_INSERT  [dbo].[RetroConfiguration] OFF
GO

-- RETROCOLUMN INSERT
SET IDENTITY_INSERT  [dbo].[RetroColumn] ON
INSERT INTO [dbo].[RetroColumn] ([Id], [RetroConfiguration_Id], [EmojiId], [Name], [Color]) VALUES (1, 1, 1, 'STOP', 'RED')
INSERT INTO [dbo].[RetroColumn] ([Id], [RetroConfiguration_Id], [EmojiId], [Name], [Color]) VALUES (2, 1, 2, 'START', 'YELLOW')
INSERT INTO [dbo].[RetroColumn] ([Id], [RetroConfiguration_Id], [EmojiId], [Name], [Color]) VALUES (3, 1, 3, 'CONTINUE', 'GREEN')
SET IDENTITY_INSERT  [dbo].[RETROCOLUMN] OFF

-- RETRO INSERT
SET IDENTITY_INSERT  [dbo].[Retro] ON
INSERT INTO [dbo].[Retro] ([Id], [ConfigurationId], [ScrumMaster], [TeamId], [Name], [Activ], [Step], [Sprint], [CreateDate])
VALUES (1, 1, 1, 3, 'Retro-1', 1, '1', '2020-10-12', '2020-10-21')
SET IDENTITY_INSERT  [dbo].[Retro] OFF