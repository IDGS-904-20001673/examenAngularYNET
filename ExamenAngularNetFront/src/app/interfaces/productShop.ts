import { ProductId } from "./product";
import { ShopId } from "./shop";

export interface ProductShop {
    idShop: Number;
    idProduct: Number;
}

export interface ProductShopId {
    id: Number;
    idShop: Number;
    idProduct: Number;
    date: Date;
    idProductNavigation: ProductId;
    idShopNavigation: ShopId;
}

