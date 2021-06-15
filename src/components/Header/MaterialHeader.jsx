import React from 'react'
import styled from './Header.module.css';
import MenuIcon from '@material-ui/icons/Menu';
import AddIcon from "@material-ui/icons/Add";
import ForumIcon from "@material-ui/icons/Forum";
import {useMemo,useCallback} from 'react';
import NotificationsActiveIcon from "@material-ui/icons/NotificationsActive";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import { IconButton ,Box,Tooltip,useMediaQuery} from '@material-ui/core';
import { useTheme } from '@material-ui/core/styles';
import {useDispatch} from 'react-redux'
import {changeSideOpen as changeSide } from '../../stores/actions/actions'
export default function MaterialHeader({sideHide}) {
    const theme=useTheme();
    const dispatch=useDispatch();
    const matches = useMediaQuery(theme.breakpoints.down('md'));
    const arrClass=useMemo(() =>{
        const arr=[styled.HeaderWrap];
        if(sideHide){
            arr.push(styled.sideHide);
        }else{
            if(matches){
               arr.push(styled.HeaderWrapMd);
            }
        }
        return arr.join(' ');

    }, [sideHide,matches])
    
    const changeSideOpen= useCallback(
        () => {
           dispatch(changeSide(!sideHide))
        },
        [dispatch,sideHide]
    )
    
    return (

             <Box className={arrClass} bgcolor="background.default">{ /*bgcolor="background.main" */}
                <Box color="primary.text" className={styled.HeaderLeft}>
                <Tooltip title={"点击打开侧边栏"}>
                    <IconButton color="inherit" onClick={changeSideOpen} > <MenuIcon></MenuIcon></IconButton>
                </Tooltip>
                </Box>
                <div className={styled.HeaderCenter}>
                    <span></span>
                </div>
                <Box color="primary.text" className={styled.HeaderRight}>
                    {/* <div className="header_info">
                    <Avatar src={user.photoURL} />
                    <h4>{user.displayName}</h4>
                </div> */}
                    <IconButton color="inherit">
                        <AddIcon />
                    </IconButton>
                    <IconButton color="inherit">
                        <ForumIcon />
                    </IconButton>
                    <IconButton color="inherit">
                        <NotificationsActiveIcon />
                    </IconButton>
                    <IconButton color="inherit">
                        <ExpandMoreIcon />
                    </IconButton>
                    
                </Box>
        </Box>
    )
}
