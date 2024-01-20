import React, { forwardRef } from 'react'
import PropTypes from 'prop-types';

import { useTheme } from '@mui/material/styles';
import { Drawer, Grid } from '@mui/material';

function EditDrawer() {
    const theme = useTheme();

    return (
        <Drawer>
            <Grid container>
                <Grid item>
                    1111111
                </Grid>
            </Grid>
        </Drawer>
    )
}

EditDrawer.propTypes = {
    className: PropTypes.string,
    color: PropTypes.string,
    outline: PropTypes.bool,
    size: PropTypes.string,
    sx: PropTypes.object,
    open: PropTypes.bool,
    anchor: PropTypes.string,
};

EditDrawer.defaultProps = {
    open: false,
    anchor: 'right'
};

export default EditDrawer