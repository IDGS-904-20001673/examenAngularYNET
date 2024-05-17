import { ProductId } from "./product";

export interface register {
    name: string;
    lastName: string;
    address: string;
    users: string;
    passsword: string;
}

export interface login {
    email: string;
    password: string;
}


export interface user {
    id: Number;
    name: string;
    lastName: string;
    address: string;
    users: string;
    passsword: string;
}


export interface buy {
    idCustomer: Number;
    idProduct: Number;
}

export interface userId {
id: Number;
idCustomer: Number;
idProduct: Number;
idCustomerNavigation: user;
idProductNavigation: ProductId;
}
