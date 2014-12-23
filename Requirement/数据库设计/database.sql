/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2014/12/23 14:35:43                          */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('Email')
            and   type = 'U')
   drop table Email
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EmailAddress')
            and   type = 'U')
   drop table EmailAddress
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EmailProfile')
            and   type = 'U')
   drop table EmailProfile
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EmailSend')
            and   type = 'U')
   drop table EmailSend
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Help')
            and   type = 'U')
   drop table Help
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Message')
            and   type = 'U')
   drop table Message
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MessageSend')
            and   type = 'U')
   drop table MessageSend
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PhoneAddress')
            and   type = 'U')
   drop table PhoneAddress
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Users')
            and   type = 'U')
   drop table Users
go

/*==============================================================*/
/* Table: Email                                                 */
/*==============================================================*/
create table Email (
   ID                   Int                  identity,
   UserID               int                  not null,
   Subject              nvarchar(500)        not null,
   Memo                 ntext                not null,
   constraint PK_EMAIL primary key (ID)
)
go

/*==============================================================*/
/* Table: EmailAddress                                          */
/*==============================================================*/
create table EmailAddress (
   ID                   Int                  identity,
   Name                 int                  not null,
   Email                nvarchar(50)         not null,
   Phone                nvarchar(100)        not null,
   constraint PK_EMAILADDRESS primary key (ID)
)
go

/*==============================================================*/
/* Table: EmailProfile                                          */
/*==============================================================*/
create table EmailProfile (
   ID                   Int                  identity,
   UserID               int                  not null,
   SMTP                 nvarchar(100)        not null,
   SMTPPort             nvarchar(100)        not null,
   Name                 nvarchar(100)        not null,
   Password             nvarchar(100)        not null,
   SSL                  int                  not null,
   constraint PK_EMAILPROFILE primary key (ID)
)
go

/*==============================================================*/
/* Table: EmailSend                                             */
/*==============================================================*/
create table EmailSend (
   ID                   Int                  identity,
   UserID               int                  not null,
   Name                 nvarchar(500)        not null,
   Memo                 ntext                not null,
   ReceiveEmail         ntext                not null,
   Date                 datetime             not null,
   constraint PK_EMAILSEND primary key (ID)
)
go

/*==============================================================*/
/* Table: Help                                                  */
/*==============================================================*/
create table Help (
   ID                   Int                  identity,
   Name                 nvarchar(100)        not null,
   Memo                 ntext                not null,
   Type                 int                  not null,
   Date                 datetime             not null,
   Status               int                  not null,
   constraint PK_HELP primary key (ID)
)
go

/*==============================================================*/
/* Table: Message                                               */
/*==============================================================*/
create table Message (
   ID                   Int                  identity,
   UserID               int                  not null,
   Name                 nvarchar(50)         not null,
   Memo                 nvarchar(500)        not null,
   constraint PK_MESSAGE primary key (ID)
)
go

/*==============================================================*/
/* Table: MessageSend                                           */
/*==============================================================*/
create table MessageSend (
   ID                   Int                  identity,
   UserID               int                  not null,
   Memo                 nvarchar(500)        not null,
   ReceivePhone         ntext                not null,
   constraint PK_MESSAGESEND primary key (ID)
)
go

/*==============================================================*/
/* Table: PhoneAddress                                          */
/*==============================================================*/
create table PhoneAddress (
   ID                   Int                  identity,
   Name                 nvarchar(50)         not null,
   Email                nvarchar(50)         not null,
   Phone                nvarchar(50)         not null,
   Password             nvarchar(50)         not null,
   RealName             nvarchar(100)        not null,
   Date                 datetime             not null,
   Status               int                  not null,
   UserGroup            int                  not null,
   Photo                nvarchar(100)        not null,
   constraint PK_PHONEADDRESS primary key (ID)
)
go

/*==============================================================*/
/* Table: Users                                                 */
/*==============================================================*/
create table Users (
   ID                   Int                  identity,
   Name                 nvarchar(50)         not null,
   Email                nvarchar(50)         not null,
   Phone                nvarchar(50)         not null,
   Password             nvarchar(50)         not null,
   RealName             nvarchar(100)        not null,
   Date                 datetime             not null,
   Status               int                  not null,
   UserGroup            int                  not null,
   Photo                nvarchar(100)        not null,
   constraint PK_USERS primary key (ID)
)
go

