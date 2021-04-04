import React, { Component } from 'react'
import InventoryService from '../services/InventoryService'


class ListInventoryComponent extends Component {
    constructor(props) {
        super(props)

        this.state = {
                products: []
        }
        this.addProduct = this.addProduct.bind(this);
        this.editProduct = this.editProduct.bind(this);
        this.deleteProduct = this.deleteProduct.bind(this);
    }

    deleteProduct(id){
        InventoryService.deleteProduct(id).then( res => {
            
             const products= [this.state.products];
             if(products.length > 1){
                 products.splice(id,1);
             }
             this.setState({products: this.state.products.filter(product => product.id !== id)});
             window.location.reload(false);
        });
    }
    viewProduct(id){
        this.props.history.push(`/view-product/${id}`);
    }
    editProduct(id){
        this.props.history.push(`/add-product/${id}`);
    }

    componentDidMount(){
        InventoryService.getProducts().then((res) => {
            this.setState({ products: res.data},()=>{
                console.log(this.state.products);
            });
        });
    }

    addProduct(){
        this.props.history.push('/add-product/_add');
    }

    render() {
        return (
            <div>
                 <h2 className="text-center">Inventory</h2>
                 <div className = "row">
                    <button className="btn btn-success" onClick={this.addProduct}><i className="fa fa-plus"></i> Add Product</button>
                 </div>
                 <br></br>
                 <div className = "row">
                        <table className = "table table-striped table-bordered">

                            <thead>
                                <tr>
                                    <th>Product</th>
                                    <th> Product Name</th>
                                    <th> Product Description</th>
                                    <th> Product Quantity</th>
                                    <th> Product Price</th>
                                    <th> Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    this.state.products.map(
                                        product => 
                                        <tr key = {product.item_Id}>
                                            
                                            <td><img src={`data:image/jpeg;base64,${product.item_Image}`} alt="product" height="100" width="100"/></td>
                                             <td> {product.item_Name} </td>   
                                             <td> {product.item_Desc}</td>
                                             <td> {product.item_Quantity}</td>
                                             <td> ₹. {product.item_Price}</td>
                                             <td>
                                                 <button onClick={ () => this.editProduct(product.item_Id)} className="btn btn-info"><i className="fa fa-edit"></i> Update </button>
                                                 <button style={{marginLeft: "10px"}} onClick={ () => this.deleteProduct(product.item_Id)} className="btn btn-danger"><i className="fa fa-trash"></i> Delete </button>
                                                 <button style={{marginLeft: "10px"}} onClick={ () => this.viewProduct(product.item_Id)} className="btn btn-info"><i className="fa fa-eye"></i> View </button>
                                             </td>
                                        </tr>
                                    )
                                }
                            </tbody>
                        </table>

                 </div>

            </div>
        )
    }
}

export default ListInventoryComponent