
import {pink,indigo,red,green,grey,blue,orange,purple,cyan} from '@mui/material/colors'
import { createTheme } from '@mui/material/styles';
// export default createMuiTheme({
//     palette:{
//         type:'light',
//         primary:{
//             main:'#000',
            
//         },
        
//         header:{
//             main:'#000'
//         },
//         background:{
          
//         },
//     },
    
//     overrides: {
//         // 样式表的名字
//         MuiIconButton:{
//             root:{
                
//             },
            
//         },
//         MuiButton: {
//           // 规则的名字
//           root:{
//               fontSize:"14px",
//           },
        
//           text: {
//             // 一些 CSS
//             color: 'white',
//           },
//         },
//     }

// })
 const THEME_TEMPLATE_KEY={
  INDIGO :'INDIGO', // DEFAULT
  RED :'RED',
  BLACK : 'BLACK',
  BLUE : 'BLUE',
  GREEN : 'GREEN',
  PINK :'PINK',
  ORANGE : 'ORANGE',
  PURPLE : 'PURPLE',
  CYAN : 'CYAN',
};

const themeObj={[THEME_TEMPLATE_KEY.INDIGO]: {
    name: '沉靛蓝',
    theme: {
   
      palette: {
        primary: {
          main: blue[500],//按钮等颜色
          text:'#fff',// 添加用于按钮文字颜色
          
        },
        secondary: pink,
        background:{
          default:blue[500],
          side:'#fff'
        },
        text:{
          primary:'#ccc',//文本颜色
          logo:'#fff'
        },
        icon:{
          primary:'rgba(0, 0, 0, 0.54)'
        },
        menu:{
          selected:'rgba(0, 0, 0, 0.04)',
        }
      },
      overrides:{
        MuiListItemIcon:{
          root:{
            minWidth:'35px',
            color:'inherit'
          }
        },
        MuiTypography:{
          body1:{
            fontSize:"14.8px"
          }
        }
      },
    }
  },
  [THEME_TEMPLATE_KEY.RED]: {
    name: '火焰红',
    theme: {
      palette: {
        primary: {
          main: red[600],
          light: red[50],
          text:'#fff'
        },
        secondary: {
          main: pink[400],
        },
        background:{
          default:red[500],
          side: red[100]
        },
        text:{
          primary:'#333',//文本颜色
        },
      },
    },
  },
  [THEME_TEMPLATE_KEY.BLACK]: {
    name: '高级黑',
    theme: {
      palette: {
     
        primary: {
          main: grey[900],
          light: grey[50],
          text:'#fff'
        },
        secondary: {
          main: '#000000',
        },
        background:{
          default:'#333',
          side:grey[900]
        },
        text:{
          primary:'#fff',//文本颜色
        },
      },
  
    },
  },
  [THEME_TEMPLATE_KEY.BLUE]: {
    name: '忧郁蓝',
    theme: {
      palette: {
        primary: {
          main: '#2196f3',
          light: blue[50],
        },
        secondary: {
          main: '#1769aa',
        },
      },
    },
  },
  [THEME_TEMPLATE_KEY.GREEN]: {
    name: '原谅绿',
    theme: {
      palette: {
        primary: {
          main: '#357a38',
          light: green[50],
        },
        secondary: {
          main: '#4caf50',
        },
      },
    },
  },
  [THEME_TEMPLATE_KEY.PINK]: {
    name: '公主粉',
    theme: {
      palette: {
        primary: {
          main: '#ed4b82',
          light: pink[50],
        },
        secondary: {
          main: '#e91e63',
        },
      },
    },
  },
  [THEME_TEMPLATE_KEY.ORANGE]: {
    name: '动感橙',
    theme: {
      palette: {
        primary: {
          main: '#ff9800',
          light: orange[50],
        },
        secondary: {
          main: '#b26a00',
        },
        
      },
    },
  },
  [THEME_TEMPLATE_KEY.PURPLE]: {
    name: '贵族紫',
    theme: {
      palette: {
        primary: {
          main: '#9500ae',
          light: purple[50],
        },
        secondary: {
          main: '#d500f9',
        },
      },
    },
  },
  [THEME_TEMPLATE_KEY.CYAN]: {
    name: '成熟青',
    theme: {
      palette: {
        primary: {
          main: '#14a37f',
          light: cyan[50],
        },
        secondary: {
          main: '#1de9b6',
        },
      },
    },
  },
}

function getTheme(themeId){
  if(themeObj[themeId])
    return createTheme(themeObj[themeId].theme);
  else
    return createTheme(themeObj[THEME_TEMPLATE_KEY.INDIGO].theme);
}

export default getTheme;
export {themeObj};
