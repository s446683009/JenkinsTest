import React from 'react'
import styled from './Header.module.css';
import MenuIcon from '@mui/material/Icon/Menu';
import AddIcon from "@mui/material/Icon/add";
import ForumIcon from "@mui/material/Icon/Forum";
import {useMemo,useCallback} from 'react';
import NotificationsActiveIcon from "@mui/material/Icon/NotificationsActive";
import ExpandMoreIcon from "@mui/material/Icon/ExpandMore";
import { IconButton ,Box,Tooltip,useMediaQuery} from '@mui/material';
import { useTheme } from '@mui/material/styles';
import {useDispatch} from 'react-redux'
import {changeSideOpen as changeSide } from '../../stores/actions/actions'
export default function MaterialHeader({sideHide}) {
    const theme=useTheme();
    const dispatch=useDispatch();
    const matches = useMediaQuery(theme.breakpoints.down('sm'));
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
