import Settings from "components/settings/settings";
import React, {Fragment} from "react";
import {Route} from "react-router";
import Home from "components/home/home";
import Logout from "components/logout/logout";

const AdminRouter = () => {
    return (
        <Fragment>
            <Route exact path='/logout' component={Logout}/>
            <Route exact path='/settings' component={Settings} />
            <Route exact path='/' component={Home}/>
        </Fragment>
    )
}

export default AdminRouter;