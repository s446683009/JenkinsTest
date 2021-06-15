import React from 'react'
import PropTypes from 'prop-types'
import styles from './Sider.module.css';
import Menu from '../Menu/index';
import {Box} from '@material-ui/core'



function Sider(props) {
    

    const isHide=props.sideHide;
    return (
        <div className={isHide?styles.wrapHide:styles.wrap}>
            <Box className={styles.header} bgcolor={"background.default"}>
                <div className={styles.logo}></div>
                <Box  color="primary.text" component={"h2"} className={styles.title} >Student Pro</Box>
            </Box>
            <Box className={styles.content} bgcolor={"background.side"} > 
                <Menu></Menu>
            </Box>
        </div>
    )
}

Sider.propTypes = {

}

export default Sider


