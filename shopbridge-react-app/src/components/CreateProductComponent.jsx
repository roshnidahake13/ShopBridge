import React, { Component } from 'react'
import InventoryService from '../services/InventoryService';
import {Alert} from 'reactstrap';
import LoadingSpinner from '../components/loadingSpinner';

class CreateProductComponent extends Component {
    constructor(props) {
        super(props)

        this.state = {
            // step 2
            id: this.props.match.params.id,
            item_Name: '',
            item_Quantity: '',
            item_Price: '',
            item_Desc:'',
            item_Image: null,
            itemAvailability:'',
            errors:{},
            loading:false
        }
        this.changeProductNameHandler = this.changeProductNameHandler.bind(this);
        this.changeProductQtyHandler = this.changeProductQtyHandler.bind(this);
        this.changeProductPriceHandler = this.changeProductPriceHandler.bind(this);
        this.changeProductDescHandler = this.changeProductDescHandler.bind(this);
        this.changeProductyAvailabilityHandler = this.changeProductyAvailabilityHandler.bind(this);
        this.onImageChange = this.onImageChange.bind(this);
       
        this.saveOrUpdateProduct = this.saveOrUpdateProduct.bind(this);
    }

     //#region EventHandlers
     changeProductNameHandler= (event) => {
        this.setState({item_Name: event.target.value});
        
    }

    changeProductQtyHandler= (event) => {
        this.setState({item_Quantity: event.target.value});
    }

    changeProductPriceHandler= (event) => {
        this.setState({item_Price: event.target.value});
    }
    changeProductDescHandler= (event) => {
        this.setState({item_Desc: event.target.value});
    }
    changeProductyAvailabilityHandler(event) {
        this.setState({itemAvailability: event.target.checked});
        //let isChecked = event.target.checked;
        
      }
    
    
     // On file select (from the pop up)
     onImageChange = event => {
    
        // Update the state
      //  this.setState({ item_Image: event.target.files[0] });
       

        var reader = new FileReader();
reader.readAsDataURL(event.target.files[0]);

reader.onload = function () {
	
    const base64String = reader.result
        .replace("data:", "")
        .replace(/^.+,/, "");
        console.log("this is base64",base64String);//base64encoded string
        this.setState({ item_Image: base64String });
}.bind(this);
    
// this.setState({
//     item_Image: URL.createObjectURL(event.target.files[0])
//   })
      
      };
   

    //#endregion

    // step 3
    componentDidMount(){

        // step 4
        if(this.state.id === '_add'){
            return
        }else{
            InventoryService.getProductById(this.state.id).then( (res) =>{
                let product = res.data;
                this.setState({
                   
        item_Name: product.item_Name,
        item_Quantity: product.item_Quantity,
        item_Price : product.item_Price,
        item_Desc : product.item_Desc,
        item_Image : product.item_Image,
        itemAvailability : product.itemAvailability
                });
            });
        }        
    }
    saveOrUpdateProduct = (e) => {


        e.preventDefault();
        this.setState({ loading: true });
        if(this.handleValidation()){
            let product = {
                item_Name: this.state.item_Name,
                item_Quantity: parseInt(this.state.item_Quantity),
                 item_Price: parseFloat(this.state.item_Price),
                  item_Desc: this.state.item_Desc,
                  item_Image: this.state.item_Image,
                  itemAvailability:  (this.state.itemAvailability)
                };
            console.log('product => ' + JSON.stringify(product));
    
            // step 5
            if(this.state.id === '_add'){
                InventoryService.createProduct(product).then(res =>{
                    this.props.history.push('/products');
                    this.setState({ loading: false });
                });
            }else{
                InventoryService.updateProduct(product, this.state.id).then( res => {
                    this.props.history.push('/products');
                    this.setState({ loading: false });
                });
            }
            //alert("Form submitted");
         }else{
             console.log("these are validation",this.state.errors);
            alert("Form has errors.")
            this.setState({ loading: false });
         }
        
         
    }
    
   

    cancel(){
        this.props.history.push('/products');
    }

    getTitle(){
        if(this.state.id === '_add'){
            return <h3 className="text-center">Add Product</h3>
        }else{
            return <h3 className="text-center">Edit Product</h3>
        }
    }

    handleValidation(){
        let item_Name = this.state.item_Name;
        let item_Quantity = this.state.item_Quantity;
        let item_Price = this.state.item_Price;
        let item_Image = this.state.item_Image;
        
        let errors = {};
        let formIsValid = true;

        //Name
        if(!item_Name){
           formIsValid = false;
           errors["name"] = "Product Name cannot be empty";
        }
  
        if(typeof (item_Name) !== "undefined"){
           if(!item_Name.match(/^[a-zA-Z0-9 ]+$/)){
              formIsValid = false;
              errors["name_validation"] = "Product Name only letters and Numbers allowed";
           }        
        }
   
        //Quantity
        if(!item_Quantity){
           formIsValid = false;
           errors["quantity"] = "Product Quantity cannot be empty";
        }

        //price
        if(!item_Price){
            formIsValid = false;
            errors["price"] = "Product Price cannot be empty";
         }



         //Description
        // if(!item_Desc){
        //     formIsValid = false;
        //     errors["desc"] = "Cannot be empty";
        //  }

         //Image
        if(!item_Image){
            formIsValid = false;
            errors["image"] = "Upload Product image.";
         }

       this.setState({errors: errors},()=>{
       // rendercontactSubmitError(this.state.errors); 
           console.log("this is validation error",this.state.errors);
       });
       return formIsValid;
   }

    isNumberKey= (price,evt) =>{
        console.log("This is is numbefr inside"+price,evt.charCode);
    var charCode =  evt.charCode;
    
    if (charCode === 46) {
      //Check if the text already contains the . character
      if (price.indexOf('.') === -1) {
        console.log("Inside this dcimal one");
        return true;
      } else {
        evt.preventDefault();
        return false;
      }
    } else {
      if  (charCode >= 48 && charCode <= 57)
        {
            console.log("Inside this");
            return true;
        }else{
            evt.preventDefault();
            return false;
        }
        
    }
    return true;
  }
    
   rendercontactSubmitError(errors){
      console.log(errors);
      console.log(this.state.errors);
      
      if(this.state.errors["name"]){
          return <Alert color="danger">
           {this.state.errors["name"] ?this.state.errors["name"]:null}
           </Alert>
          
      }
      else if(this.state.errors["name_validation"]){
        return <Alert color="danger">
         {this.state.errors["name_validation"] ? this.state.errors["name_validation"]:null}
         </Alert>
        
    }
    else if(this.state.errors["quantity"]){
        return <Alert color="danger">
         {this.state.errors["quantity"] ? this.state.errors["quantity"]:null}
         </Alert>
        
    }
    else if(this.state.errors["price"]){
        return <Alert color="danger">
         {this.state.errors["price"] ? this.state.errors["price"]:null}
         </Alert>
        
    }
    else if(this.state.errors["image"]){
        return <Alert color="danger">
         {this.state.errors["image"] ? this.state.errors["image"]:null}
         </Alert>
        
    }
        // return (
        
        //    errors ? 
        //    <Alert color="warning">Following fields are empty:<br/>
        //    {this.state.errors["name"] ? "<br/>"+this.state.errors["name"]:null}
        //    <br/>{this.state.errors["name_validation"] ? this.state.errors["name_validation"]:null}
        //    <br/>{this.state.errors["quantity"] ?this.state.errors["quantity"]:null}      
        //    <br/>{this.state.errors["price"] ?this.state.errors["price"]:null}
        //    <br/>{this.state.errors["image"] ?this.state.errors["image"]:null}
          
        //    </Alert>:null
        //   );
       
    }

    renderImage(){
       return(
        <div className = "form-group">
        <img src={`data:image/jpeg;base64,${this.state.item_Image}`} alt="product" height="100" width="100"/>
           </div>
       )
    }
    render() {
        const { loading } = this.state;
        
        return (
            <div>
                <br></br>
                   <div className = "container">
                        <div className = "row">
                            <div className = "card col-md-6 offset-md-3 offset-md-3">
                                {
                                    this.getTitle()
                                }
                                <div className = "card-body">
                                    <form>
                                    {this.rendercontactSubmitError(this.state.errors)}
                                        <div className = "form-group">
                                            <label> Product Name<span style={{color:"red"}}>*</span>: </label>
                                            <input placeholder="Product Name" name="productName" className="form-control" 
                                                value={this.state.item_Name} onChange={this.changeProductNameHandler}/>
                                        </div>
                                        <div className = "form-group">
                                            <label> Product Description: </label>
                                            <input placeholder="Product Description" name="productDesc" className="form-control" 
                                                value={this.state.item_Desc} onChange={this.changeProductDescHandler}/>
                                        </div>
                                        <div className = "form-group">
                                            <label> Product Quantity<span style={{color:"red"}}>*</span>: </label>
                                            <input type="number" min="0" placeholder="Product Quantity" name="productQty" className="form-control" 
                                                value={this.state.item_Quantity} onChange={this.changeProductQtyHandler}/>
                                        </div>
                                        <div className = "form-group">
                                            <label> Product Price<span style={{color:"red"}}>*</span>: </label>
                                            <input placeholder="Product Price" type="text" name="productPrice" className="form-control" 
                                                value={this.state.item_Price} onChange={this.changeProductPriceHandler} onKeyPress={(e) => this.isNumberKey(this.state.item_Price, e)} />
                                        </div>
                                    
                                        <div className = "form-group">
                                            <label> Product Availability: </label>
                                            <input type="checkbox" name="productAvailability" className="form-check" 
                                               checked={this.state.itemAvailability === true ? "checked":""} onChange={this.changeProductyAvailabilityHandler}/>
                                        </div>

                                        <div className = "form-group">
                                            <label> Product Image<span style={{color:"red"}}>*</span>: </label>
                                            <input type="file" className="form-control" onChange={this.onImageChange}/>
                                        </div>
                                        
                                        {
                                        this.state.item_Image ? this.renderImage():null
                                            
                                        }
                                        
                                        {loading ? <LoadingSpinner /> : <button className="btn btn-success" onClick={this.saveOrUpdateProduct} >Save</button>}
                                        <button className="btn btn-danger" onClick={this.cancel.bind(this)} style={{marginLeft: "10px"}}>Cancel</button>
                                    </form>
                                </div>
                            </div>
                        </div>

                   </div>
            </div>
        )
    }
}



export default CreateProductComponent