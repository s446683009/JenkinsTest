import React, { useState, useEffect, useMemo, useCallback } from 'react'
import PropTypes from 'prop-types'
import { ChevronRight, ExpandMore as ExpandMoreIcon } from '@mui/material/Icon';
import { Button, Box,List,ListItem,ListItemIcon,ListItemText,Collapse} from '@mui/material';
import styled from './Menu.module.css';
import InboxIcon from '@mui/material/Icon/MoveToInbox';
import DraftsIcon from '@mui/material/Icon';
import SendIcon from '@mui/material/Icon/Send';
import ExpandLess from '@mui/material/Icon/ExpandLess';
import ExpandMore from '@mui/material/Icon/ExpandMore';
import StarBorder from '@mui/material/Icon/StarBorder';
import { makeStyles } from '@mui/material/styles';
import menus from '../../util/getMenu'


const useStyles=makeStyles((theme)=>({
    root:{
        color:theme.palette.icon.primary,
        
    },
    selected:{
        background:theme.palette.menu.selected
    }
    
}))

const mapList=(menu,options,padding=16)=>{
    const {iconclass,openKey,selectMenuId,onMenuClick,onMenuExpend}=options;
    
    return menu.map(function(t){
        const bl=openKey.includes(t.menuId);
        return (
            <div key={t.menuId}>
                <ListItem  style={{ paddingLeft: `${padding}px` }} className={selectMenuId==t.menuId?iconclass.selected:''} button onClick={onMenuClick.bind(null,t.menuId,t.hasChildren)}>

                    <ListItemIcon >
                        <InboxIcon />
                    </ListItemIcon>

                    <ListItemText primary={t.menuName} />
                    {t.hasChildren?
                        bl?<ExpandMore  />:<ExpandLess />
                    :null}
                </ListItem>
                {
                    t.hasChildren?
                        <Collapse  in={bl} timeout="auto" unmountOnExit >
                          {mapList(t.children,options,padding+16)}
                        </Collapse>
                    : null
                }
            </div>
        )

    })

}

function CustomMenu() {
 
    const [selectMenuId, setselectMenuId] = useState('')
    const [expendMenu, setExpendMenu] = useState([])
    const classes=useStyles();
    const handleOpen=useCallback((menuKey,e)=>{
            setExpendMenu((menu)=>{
            let index=menu.findIndex(t=>t===menuKey);

              if(index<0){
                return [...menu,menuKey];
              }
              
               menu.splice(index,1);
               //不能返回原对象，比较指针会认为没有改变
               return [...menu];
            });

         
        
    },[])
    const handClick=useCallback(
        (menuId,hasChildren,e) => {
            if(hasChildren){
                handleOpen(menuId,e);
            }
            setselectMenuId(menuId)
        },
        []
    )

    return (
        <Box    className={styled.tree_wrap}>
            <List
                component="nav"
                aria-labelledby="nested-list-subheader"
            >
                {
                    mapList(menus,{
                        openKey:expendMenu,
                        selectMenuId:selectMenuId,
                        iconclass:classes,
                        onMenuExpend:handleOpen,
                        onMenuClick:handClick
                    })
                }
            </List>
        </Box>
    )
}




CustomMenu.propTypes = {

}






export default React.memo(CustomMenu)

