import React,{Component} from 'react';
import {variable} from './Variable.js';

export class List extends Component
{
    constructor(props){
        super(props);
        this.state={
            Products:[],
            modalTitle:"",
            ProductId:0,
            ProductName:"",
            ProductCount:"",
            ProductCost:""
        }
    }
    
    refreshList(){
        fetch(variable.API_URL+'list')
        .then(response=>response.json())
        .then(data=>{
            this.setState({Products:data});
        })
    }

    componentDidMount(){
        this.refreshList();
    }
     
    changeName=(e)=>{
        this.setState({ProductName:e.target.value});
    }
    changeCount=(e)=>{
        this.setState({ProductCount:e.target.value});
    }
    changeCost=(e)=>{
        this.setState({ProductCost:e.target.value});
    }

    addClick(){
        this.setState({
            modalTitle:"Add Product",
            ProductId:0,
            ProductName:"",
            ProductCount:1,
            ProductCost:""
        });
    }
    editClick(l){
        this.setState({
            modalTitle:"Edit Product",
            ProductId:l.ProductId,
            ProductName:l.ProductName,
            ProductCount:l.ProductCount,
            ProductCost:l.ProductCost
        });
    }
    createClick(){
        fetch(variable.API_URL+'list',{
            method:'POST',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                ProductName:this.state.ProductName,
                ProductCount:this.state.ProductCount,
                ProductCost:this.state.ProductCost
            })
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
    }
    updateClick(){
        fetch(variable.API_URL+'list',{
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                ProductId:this.state.ProductId,
                ProductName:this.state.ProductName,
                ProductCount:this.state.ProductCount,
                ProductCost:this.state.ProductCost
            })
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
    }

    deleteClick(id){
        if(window.confirm('Are you sure?')){
        fetch(variable.API_URL+'list/'+id,{
            method:'DELETE',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            }
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed');
        })
    }
    }
    incrementClick(id){
        fetch(variable.API_URL+'list/'+id+'/increment',{
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            }
            
        })
        .then(()=>{
            this.refreshList();})
        
    }
    decreaseClick(id, count){
        fetch(variable.API_URL+'list/'+id+'/'+count+'/decrease',{
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            }
        })
        .then(()=>{
            this.refreshList();})
    }
    
    render(){
        const {
            ProductId,
            Products,
            modalTitle,
            ProductName,
            ProductCount,
            ProductCost
        }=this.state;
        return(
            <div>
                <button type="button" className="btn btn-primary m-2 float-end"
                data-toggle="modal"
                data-target="#exampleModal"
                onClick={()=>this.addClick()}>
                    Add Product
                </button>

                <table className="table table-stripped">
                    <thead>
                        <tr>
                            <th>
                                ID
                            </th>
                            <th>
                                Name
                            </th>
                            <th>
                                Count
                            </th>
                            <th>
                                Cost per 1 product
                            </th>
                            <th>
                                Options
                            </th>
                            <th>
                                Summary cost
                            </th>
                            
                        </tr>
                    </thead>
                    <tbody>
                        {Products.map(l=>
                            <tr key={l.ProductId}>
                                <td>{l.ProductId}</td>
                                <td>{l.ProductName}</td>
                                <td>{l.ProductCount}</td>
                                <td>{l.ProductCost}</td>
                                <td>
                                <button type="button" className="btn btn-light mr-1"
                                data-toggle="modal"
                                data-target="#exampleModal"
                                onClick={()=>this.editClick(l)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil" viewBox="0 0 16 16">
                                    <path d="M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207 11.207 2.5zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293l6.5-6.5zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z"/>
                                    </svg>
                                </button>
                                <button type="button" className="btn btn-light mr-1"
                                    onClick={()=>this.deleteClick(l.ProductId)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash" viewBox="0 0 16 16">
                                    <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
                                    <path fillRule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
                                    </svg>
                                    
                                </button>
                                <button type="button" className="btn btn-light mr-1"
                                    onClick={()=>this.incrementClick(l.ProductId)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-file-plus" viewBox="0 0 16 16">
                                        <path d="M8.5 6a.5.5 0 0 0-1 0v1.5H6a.5.5 0 0 0 0 1h1.5V10a.5.5 0 0 0 1 0V8.5H10a.5.5 0 0 0 0-1H8.5V6z"/>
                                        <path d="M2 2a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V2zm10-1H4a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1z"/>
                                        </svg>
                                </button>
                                <button type="button" className="btn btn-light mr-1"
                                    onClick={()=>this.decreaseClick(l.ProductId, l.ProductCount)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-file-minus" viewBox="0 0 16 16">
                                        <path d="M5.5 8a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 0 1H6a.5.5 0 0 1-.5-.5z"/>
                                        <path d="M4 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H4zm0 1h8a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1z"/>
                                        </svg>
                                </button>
                                </td>
                                <td>
                                    {l.ProductCost * l.ProductCount}
                                </td>
                            </tr>
                            )}
                    </tbody>
                </table>
            <div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
                <div className="modal-dialog modal-lg modal-dialog-centered">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title">{modalTitle}</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                            <div classname="modal-body">
                                <div className="d-flex flex-row bd-highlight mb-3">
                                    
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Name</span>
                                            <input type="text" className="form-control"
                                            value={ProductName}
                                            onChange={this.changeName}/>
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Count</span>
                                            <input type="text" className="form-control"
                                            value={ProductCount}
                                            onChange={this.changeCount}/>
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Cost</span>
                                            <input type="text" className="form-control"
                                            value={ProductCost}
                                            onChange={this.changeCost}/>
                                        </div>
                                    
                                    
                                {ProductId==0?
                                <button type="button" className="btn btn-primary float-start"
                                onClick={()=>this.createClick()}
                                >Create</button>:null}

                                {ProductId!=0?
                                <button type="button" className="btn btn-primary float-start"
                                onClick={()=>this.updateClick()}
                                >Update</button>:null}
                                </div>
                        </div>
                    </div>
                </div>
            </div> 
        </div>
        
        )
    }
}