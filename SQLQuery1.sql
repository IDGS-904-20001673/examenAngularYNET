create database dbAngularNet;
use dbAngularNet;


create table users (
id int primary key identity,
name varchar(50),
lastName varchar(50),
address varchar(50),
users varchar(50),
passsword varchar(50)
);

create table shop (
id int primary key identity,
branch varchar(50),
address varchar(50)
);

create table products (
id int primary key identity,
code varchar(50),
description varchar(50),
price float,
img nvarchar(max),
stock int
);

create table product_shop (
id int primary key identity,
idShop int,
idProduct int,
date datetime,
FOREIGN KEY (idShop) REFERENCES shop(id),
FOREIGN KEY (idProduct) REFERENCES products(id)

);


create table customer_product (
id int primary key identity,
idCustomer int,
idProduct int,
FOREIGN KEY (idCustomer) REFERENCES users(id),
FOREIGN KEY (idProduct) REFERENCES products(id)
);
