import {Chnage_SIDEISOPEN} from '../actions/actionTypes'

//边状态
const side={
    isHide:false
};
export default function sideReducer(preState=side,action){
    switch (action.type) {
        case Chnage_SIDEISOPEN:
            return {...preState,isHide:action.payload};
           
        default:
            return preState;
    }
}