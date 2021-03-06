import store from 'stores/Store';
import {http} from "common/http";
import {login, logout} from "features/userSlice";
import {ServiceBase} from "services/base/serviceBase";
import {AxiosResponse, CancelToken} from "axios";
import {LoginInput} from "services/input/login/loginInput";
import {LoginResult} from "services/result/login/loginResult";
import {RegisterInput} from "services/input/register/registerInput";

export class AuthService extends ServiceBase {
    public async login(input: LoginInput, cancelToken?: CancelToken) {
        await http.post('/Authentication/Authenticate', input, { cancelToken: cancelToken })
            .then((response: AxiosResponse<LoginResult>) => AuthService.doLogin(response))
            .catch((e: any) => this.onError(e));
    }

    public async register(input: RegisterInput, cancelToken?: CancelToken) {
        await http.post('/Authentication/Register', input, { cancelToken })
            .catch((e: any) => this.onError(e));
    }

    public logout() {
        store.dispatch(logout());
    }

    private static doLogin(response: AxiosResponse<LoginResult>) {
        store.dispatch(login(response.data));
    }
}
