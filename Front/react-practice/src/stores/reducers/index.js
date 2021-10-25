import {combineReducers} from 'redux'
import theme from './theme';
import sideReducer from './side'
export default combineReducers({
    theme,
    sideState:sideReducer

});