import React, { Suspense } from 'react'
import Loading from '../components/Loading'
const HomeComponent=React.lazy(()=>(import('./Home/index')))

//home
const  CommonWrap=(Component)=>{
    return function Wrap(props){
        return (
            <Suspense fallback={Loading}>
                <Component {...props} />
            </Suspense>

        )

    }

}
//首页
export const Home=CommonWrap(HomeComponent);
