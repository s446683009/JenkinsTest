

import {CHANGE_THEMEID} from '../actions/actionTypes'

 const themeReducer=(previousState='',action)=>{
    switch (action.type) {
        case CHANGE_THEMEID:
            return action.payload;
        default:
            return previousState;
    }
}
export default themeReducer;