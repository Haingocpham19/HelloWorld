import { combineReducers } from 'redux';  // Import combineReducers to combine multiple reducers into a single root reducer
import { persistReducer, persistStore, PersistConfig } from 'redux-persist';  // Import redux-persist to add persistence to the Redux store
import storage from 'redux-persist/lib/storage';  // Import default storage (localStorage) for persistence
import { setAuthToken } from '../helpers';  // Import a custom helper to set the authentication token
import accountReducer from './account/reducers';  // Import account reducer
import alertReducer from './alert/reducers';  // Import alert reducer
import notificationReducer from './notification/reducers';  // Import notification reducer
import usersReducer from './users/reducers';  // Import users reducer
import { configureStore } from '@reduxjs/toolkit';  // Import configureStore from @reduxjs/toolkit for simplified store setup

// Define the state interface for the entire Redux state tree
export interface RootState {
  account: any;
  users: any;
  alert: any;
  notification: any;
}

// Configuration object for redux-persist
const persistConfig: PersistConfig<RootState> = {
  key: 'root',  // Key for the persisted reducer
  storage,  // Storage engine
  whitelist: ['account'],  // List of reducers to persist
};

// Combine all reducers into a single root reducer
const rootReducer = combineReducers<RootState>({
  account: accountReducer,
  users: usersReducer,
  alert: alertReducer,
  notification: notificationReducer,
});

// Wrap the rootReducer with persistReducer to add persistence capabilities
const persistedReducer = persistReducer(persistConfig, rootReducer);

// Configure the Redux store using configureStore from @reduxjs/toolkit
const store = configureStore({
  reducer: persistedReducer,  // Set the persisted reducer
});

// Create the persisted store
const persistedStore = persistStore(store);

// Initialize the current state
let currentState = store.getState() as RootState;

// Subscribe to store updates to handle token changes
store.subscribe(() => {
  let previousState = currentState;
  currentState = store.getState() as RootState;
  // Check if the account token has changed
  if (previousState.account.token !== currentState.account.token) {
    const token = currentState.account.token;
    if (token) {
      setAuthToken(token);  // Set the authentication token if it exists
    }
  }
});

// Export the configured store and persisted store
export { persistedStore, store };

export default rootReducer;
