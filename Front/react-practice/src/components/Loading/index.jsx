import React from 'react'
import PropTypes from 'prop-types'
import styles from './load.module.css';

function Loading({title="loading"}) {
    return (
        <div className={styles.Wrap}>
            <div className={styles.Box}>
                {title}
            </div>
        </div>
    )
}

Loading.propTypes = {
    title:PropTypes.string,
}


export default Loading

