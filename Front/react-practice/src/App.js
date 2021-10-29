import {useMemo} from 'react'
import {Switch,HashRouter as Router} from 'react-router-dom'
import {renderRoutes} from 'react-router-config';
import routes from './router-config'
import getTheme from './util/getTheme'
import { ThemeProvider } from '@mui/material/styles';
import {useSelector} from 'react-redux'


function App() {
    const themeId=useSelector(state=>state.theme);
    const theme=useMemo(() => getTheme(), [])
    return (
        <ThemeProvider theme={theme}>
        <Router>
            <Switch>
                {renderRoutes(routes)}
            </Switch>
        </Router>  
        </ThemeProvider>
    )
    
}

export default App;
