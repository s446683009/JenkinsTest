import React,{useMemo}from 'react'
import PropTypes from 'prop-types'
import styles from './Sider.module.css';
import Menu from '../Menu/index';
import {Box,useMediaQuery} from '@mui/material'
import {useTheme} from '@mui/material'


function Sider(props) {
    const theme=useTheme();
    const isSd=useMediaQuery(theme.breakpoints.down('sm'));
    const isHide=props.sideHide;
    const wrapClass=useMemo(() => {
        if(isHide){
            return styles.wrapHide;
        }else{
         
                return styles.wrap
         
        }
    },[isHide])

    return (
        <div className={`${wrapClass}`}>
            <Box className={styles.header} bgcolor={"background.default"}>
                <div className={styles.logo}></div>
                <Box   component={"h2"} className={styles.title} color="text.logo" >Student Pro</Box>
            </Box>
            <Box className={styles.content} bgcolor="background.side"  > 
                <Menu></Menu>
            </Box>
        </div>
    )
}

Sider.propTypes = {

}

export default Sider


