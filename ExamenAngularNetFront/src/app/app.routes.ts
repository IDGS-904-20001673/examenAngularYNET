import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'products',
        loadComponent: () => import('./views/products/products.component').then(c => c.ProductsComponent),
        title: 'Productos',
    },
    {
        path: 'shops',
        loadComponent: () => import('./views/shop/shop.component').then(s => s.ShopComponent),
        title: 'Shops',
    },
    {
        path: 'productshops',
        loadComponent: () => import('./views/product-shop/product-shop.component').then(ps => ps.ProductShopComponent),
        title: 'Products Shop',
    },
    {
        path: 'login',
        loadComponent: () => import('./views/login/login.component').then(l => l.LoginComponent),
        title: 'Login',
    },
    {
        path: 'register',
        loadComponent: () => import('./views/register/register.component').then(r => r.RegisterComponent),
        title: 'Registrar',
    },
    {
        path: 'productsUser',
        loadComponent: () => import('./views/products-user/products-user.component').then(pu => pu.ProductsUserComponent),
        title: 'Comprar',
    },
    {
        path: 'carrito',
        loadComponent: () => import('./views/carrito/carrito.component').then(c => c.CarritoComponent),
        title: 'Comprar',
    },
    {
        path: 'myBuys',
        loadComponent: () => import('./views/my-buys/my-buys.component').then(mb => mb.MyBuysComponent),
        title: 'mis Compras',
    },
    {
        path: '**',
        redirectTo: 'products'
    }
];
