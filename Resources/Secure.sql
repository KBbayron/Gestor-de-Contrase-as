create database SecureKenGenData COLLATE SQL_Latin1_General_CP1_CI_AS;
use SecureKenGenData;
create table Enter(
id int IDENTITY(1,1) primary key not null,
per varchar (10) not null,
word smallint not null,
stateLog bit not null
);
create table Labels(
Id int IDENTITY(1,1) primary key not null,
used int not null,
lbl varchar(25) not null,
pass varchar(25) not null
foreign key (used) references Enter (id)
);
