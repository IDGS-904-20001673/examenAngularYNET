import { user } from "./auth";

export interface Product {
    code: string;
    description: string;
    price: Number;
    img: string;
    stock: Number;
}

export interface ProductId {
    id: Number;
    code: string;
    description: string;
    price: Number;
    img: string;
    stock: Number;
}


