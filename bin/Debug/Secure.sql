create database SecureKenGenData COLLATE SQL_Latin1_General_CP1_CI_AS;
use SecureKenGenData;

create table Labels(
Id int IDENTITY(1,1) primary key not null,
lbl varchar(25),
pass varchar(25)
);

