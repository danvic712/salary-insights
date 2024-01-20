import React from "react";

import {
    Button,
    IconButton,
    Grid,
    TextField,
    TablePagination,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    Drawer,
    Card,
    CardActionArea,
    CardContent,
    CardActions,
    Typography,
    FormControlLabel,
    Checkbox
} from "@mui/material";

import { gridSpacing, rowSpacing } from '@/store/constant';


import MainCard from "@/components/cards/MainCard";
import EditDrawer from "./EditDrawer";

import { DeleteOutline, FavoriteOutlined } from "@mui/icons-material";

import { useTheme } from '@mui/material/styles';

const cards = [{
    id: 1,
    name: "Nature Around Us",
    description:
        "We are going to learn different kinds of species in nature that live together to form amazing environment."
}, {
    id: 2,
    name: "Nature Around Us",
    description:
        "We are going to learn different kinds of species in nature that live together to form amazing environment."
}, {
    id: 3,
    name: "Nature Around Us",
    description:
        "We are going to learn different kinds of species in nature that live together to form amazing environment."
}, {
    id: 4,
    name: "Nature Around Us",
    description:
        "We are going to learn different kinds of species in nature that live together to form amazing environment."
}, {
    id: 5,
    name: "Nature Around Us",
    description:
        "We are going to learn different kinds of species in nature that live together to form amazing environment."
}, {
    id: 6,
    name: "Nature Around Us",
    description:
        "We are going to learn different kinds of species in nature that live together to form amazing environment."
}, {
    id: 7,
    name: "Nature Around Us",
    description:
        "We are going to learn different kinds of species in nature that live together to form amazing environment."
}, {
    id: 8,
    name: "Nature Around Us",
    description:
        "We are going to learn different kinds of species in nature that live together to form amazing environment."
}, {
    id: 9,
    name: "Nature Around Us",
    description:
        "We are going to learn different kinds of species in nature that live together to form amazing environment."
}, {
    id: 10,
    name: "Nature Around Us",
    description:
        "We are going to learn different kinds of species in nature that live together to form amazing environment."
}];

export default function PortfolioSummary() {
    const theme = useTheme();

    const firstPageLoad = React.useRef(true);

    const [state, setState] = React.useState({
        keywords: '',
        sorting: 'asc',
        favorite: false,
        pageIndex: 0,
        pageSize: 16,
        anchor: false
    });

    const handleKeywordsChange = (event) => {
        setState({
            ...state,
            keywords: event.target.value
        })
    }

    const handleSortingChange = (event) => {
        setState({
            ...state,
            sorting: event.target.value
        })
    };

    const handleChangePage = (event, page) => {
        setState({
            ...state,
            pageIndex: page
        })
    }

    const handleChangeRowsPerPage = (event) => {
        setState({
            ...state,
            pageSize: 0,
            pageSize: event.target.value,
        })
    }

    React.useEffect(() => {
        if (firstPageLoad.current) {
            firstPageLoad.current = false
            return
        }

        handleSubmit(event);
    }, [state])

    const handleSubmit = (event) => {
        if (event !== undefined) {
            event.preventDefault();
        }

        console.log(state);
    }

    const toggleDrawer = (anchor, open) => (event) => {
        debugger;
        if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
            return;
        }

        setState({ ...state, [anchor]: open });
    };

    return (
        <>
            <Grid container gridSpacing={gridSpacing}>
                <Grid item xs={12}>
                    <MainCard component="form" noValidate onSubmit={handleSubmit} sx={{ mb: 2 }}>
                        <Grid container direction="row" spacing={rowSpacing}>
                            <Grid item xs={6}>
                                <TextField
                                    id="keywords"
                                    label="Keywords"
                                    fullWidth
                                    size="small"
                                    value={state.keywords}
                                    onChange={handleKeywordsChange}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <FormControl fullWidth size="small">
                                    <InputLabel id="sorting-select-label">Sorting By Name</InputLabel>
                                    <Select
                                        labelId="sorting-select-label"
                                        id="sorting"
                                        value={state.sorting}
                                        defaultValue={"asc"}
                                        label="Sorting By Name"
                                        onChange={handleSortingChange}
                                    >
                                        <MenuItem value={"asc"}>ASC</MenuItem>
                                        <MenuItem value={"desc"}>DESC</MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={2}>
                                <FormControlLabel
                                    control={<Checkbox value={state.favorite} />}
                                    label="Favorite Only"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <Button
                                    variant="contained"
                                    type="submit">
                                    Search
                                </Button>
                            </Grid>
                            <Grid item xs={2}>
                                <Button
                                    variant="contained"
                                    color="secondary"
                                    onClick={toggleDrawer(state.anchor, true)}>
                                    Create
                                </Button>
                                <EditDrawer></EditDrawer>
                            </Grid>
                        </Grid>
                    </MainCard>
                </Grid>
                <Grid item xs={12}>
                    <Grid container spacing={gridSpacing}>
                        <Grid item xs={12}>
                            <MainCard>
                                <Grid container direction="row" spacing={rowSpacing}>
                                    {cards.map((cards, index) => {
                                        const { image, name, description } = cards;
                                        return (
                                            <Grid item xs={3}>
                                                <Card variant="outlined" sx={{
                                                    border: '1px solid',
                                                    borderColor: theme.palette.primary.light,
                                                    ':hover': {
                                                        boxShadow: '0 2px 14px 0 rgb(32 40 45 / 8%)'
                                                    }
                                                }}>
                                                    <CardActionArea>
                                                        <CardContent>
                                                            <Typography gutterBottom variant="h5" component="div">
                                                                {name}
                                                            </Typography>
                                                            <Typography variant="body2" color="text.secondary">
                                                                {description}
                                                            </Typography>
                                                        </CardContent>
                                                    </CardActionArea>
                                                    <CardActions sx={{ p: 1.25, pt: 0 }}>
                                                        <IconButton aria-label="Favorite" title="Favorite">
                                                            <FavoriteOutlined />
                                                        </IconButton>
                                                        <IconButton aria-label="Delete" title="Delete">
                                                            <DeleteOutline />
                                                        </IconButton>
                                                    </CardActions>
                                                </Card>
                                            </Grid>
                                        );
                                    })}
                                </Grid>
                                <Grid>
                                    <TablePagination
                                        component="div"
                                        showFirstButton
                                        showLastButton
                                        count={cards.length}
                                        page={state.pageIndex}
                                        rowsPerPageOptions={[16, 24, 32]}
                                        onPageChange={handleChangePage}
                                        rowsPerPage={state.pageSize}
                                        onRowsPerPageChange={handleChangeRowsPerPage}
                                        sx={{ mb: -3 }}
                                    />
                                </Grid>
                            </MainCard>
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </>
    )
};