import React, { Component } from 'react'
import InventoryService from '../services/InventoryService'

class ViewInventoryComponent extends Component {
    constructor(props) {
        super(props)

        this.state = {
            id: this.props.match.params.id,
            product: {}
        }
    }

    componentDidMount(){
        InventoryService.getProductById(this.state.id).then( res => {
            this.setState({product: res.data});
        })
    }

    renderImage(){
        return(
            <div className = "row">
                <div className="col-md-5"><label className="label-bold"> Product Image: </label></div>
                 <div className="col-md-7"><img src={`data:image/jpeg;base64,${this.state.product.item_Image}`} alt="product" height="100" width="100"/></div>
            
            
        </div>
        )
     }

    render() {
        return (
            <div>
                <br></br>
                <div className = "card col-md-7 offset-md-3">
                    <h3 className = "text-center"> View Product Details</h3>
                    <div className = "card-body" >
                        <div className = "row">
                            <div className="col-md-5"><label className="label-bold"> Product Name: </label></div>
                            <div className="col-md-7"> <div> { this.state.product.item_Name }</div></div>
                            
                           
                        </div>
                        <div className = "row">
                        <div className="col-md-5"><label className="label-bold"> Product Description: </label></div>
                            <div className="col-md-7"> <div> { this.state.product.item_Desc }</div></div>
                            
                           
                        </div>
                        <div className = "row">
                        <div className="col-md-5"><label  className="label-bold"> Product Price:  </label></div>
                            <div className="col-md-7"><div > ₹ { this.state.product.item_Price }</div></div>
                            
                            
                        </div>
                        <div className = "row">
                        <div className="col-md-5"><label className="label-bold"> Product Quantity: </label></div>
                            <div className="col-md-7"> <div> { this.state.product.item_Quantity }</div></div>
                            
                           
                        </div>
                        {
                             this.state.product.item_Image ? this.renderImage():null

                        }
                        {/* <div className = "row">
                            <label className="label-bold"> Product Image: </label>
                            <img src={`data:image/jpeg;base64,${this.state.product.item_Image}`} alt="product" height="100" width="100"/>
                        </div> */}
                       
                    </div>

                </div>
            </div>
        )
    }
}

export default ViewInventoryComponent