import { RootState, store } from '../store';
import {
  REFRESH_TOKEN_FAILURE,
  REFRESH_TOKEN_REQUEST,
  REFRESH_TOKEN_SUCCESS,
} from '../store/account/types';

import { UrlConstants } from '../constants';
import env from 'react-dotenv';
import { history } from './history';

/**
 * Intercept any error responses from the API
 * and check if the token is no longer valid.
 * Logout the user if the token has expired.
 */
interface RequestOptions extends RequestInit {
  url: string;
}

const api = {
  request: async <T>(url: string, options: RequestOptions): Promise<T | void> => {
    try {
      const response = await fetch(url, options);

      if (!response.ok) {
        throw response;
      }

      return response.json();
    } catch (error: any) {
      if (error.status === 401) {
        const currentState = store.getState() as RootState;
        const refreshToken = currentState.account.refreshToken;

        // Prevent infinite loops
        if (
          error.status === 401 &&
          options.url === `${env.API_URL}/api/v1/auth/refresh-token/`
        ) {
          history.push(UrlConstants.LOGIN);
          return Promise.reject(error);
        }

        if (
          error.statusText === 'Unauthorized' &&
          error.status === 401 &&
          error.message === 'Token is expired'
        ) {
          if (refreshToken) {
            store.dispatch({
              type: REFRESH_TOKEN_REQUEST,
            });

            const refreshOptions: RequestOptions = {
              url: `${env.API_URL}/api/v1/auth/refresh-token/`,
              method: 'POST',
              headers: {
                'Content-Type': 'application/json',
              },
              body: JSON.stringify({ refreshToken }),
            };

            return api
              .request<T>('/v1/auth/refresh-token', refreshOptions)
              .then((response: any) => {
                store.dispatch({
                  type: REFRESH_TOKEN_SUCCESS,
                  payload: {
                    token: response.token,
                    refreshToken: response.refreshToken,
                  },
                });

                return api.request<T>(options.url, {
                  ...options,
                  headers: {
                    ...options.headers,
                    'x-auth-token': response.token,
                  },
                });
              })
              .catch((err) => {
                store.dispatch({
                  type: REFRESH_TOKEN_FAILURE,
                  payload: {
                    error: err.toString(),
                  },
                });

                history.push(UrlConstants.LOGIN);
              });
          } else {
            console.log('Refresh token not available.');
            history.push(UrlConstants.LOGIN);
          }
        }
      }

      return Promise.reject(error);
    }
  },
};

export { api };
