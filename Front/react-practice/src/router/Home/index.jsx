import {useMemo} from 'react';
import PropTypes from 'prop-types'
import { Button,useMediaQuery,useTheme,Hidden,Backdrop} from '@mui/material'
import styled from './Home.module.css'
import Header from '../../components/Header/MaterialHeader';
import Sider from '../../components/Sider';
import {useSelector} from 'react-redux'
function useSide(){
    const sideState=useSelector(state=>state.sideState);
    
    return sideState.isHide;
}

function Home(props) {
    const sideHide=useSide();
    const theme = useTheme();
    const matches = useMediaQuery(theme.breakpoints.down('sm'));
    const arrClass=useMemo(() =>{
        const arr=[styled.content];
        if(sideHide){
            arr.push(styled.sideHide);
        }else{
            if(matches){
               arr.push(styled.contentMd);
            }
        }
        return arr.join(' ');

    }, [sideHide,matches])
    return (
        <div>
            {matches?
            <Backdrop  open={!sideHide&&matches}>
                <Sider sideHide={sideHide} />
            </Backdrop>:
                <Sider sideHide={sideHide} />
            }
            <section  className={arrClass}>
                    <Header sideHide={sideHide}></Header>
            </section> 

          
        </div>
    )
}

Home.propTypes = {

}

export default Home

