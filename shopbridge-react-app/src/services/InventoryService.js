import axios from 'axios';

const INVENTORY_API_BASE_URL = "http://localhost:53827/api/";

class InventoryService {

    getProducts(){
        const data=axios.get(INVENTORY_API_BASE_URL+'getInventoryItemList');
        console.log("This is service",data);
        return data;
    }

    createProduct(inventory){
        console.log("In service add",inventory);
        return axios.post(INVENTORY_API_BASE_URL+"PostInventoryItem", inventory);
    }

    getProductById(productId){
     const data= axios.get(INVENTORY_API_BASE_URL + 'getInventoryItem?InventoryId=' + productId);
     console.log("In service getbyID",data);
     return data;
    }

    updateProduct(product, productId){
       
        
        return axios.put(INVENTORY_API_BASE_URL + "PutInventoryItem?id=" + productId,product);
    }

    deleteProduct(productId){
        console.log("productID",productId);
        return axios.delete(INVENTORY_API_BASE_URL + 'DeleteInventoryItem?itemId=' + productId);
    }
}

export default new InventoryService()