import {
  ADD_NOTIFICATION,
  CLEAR_NOTIFICATION,
  MARK_ALL_AS_READ,
  MARK_AS_READ,
  NotificationActionTypes,
  NotificationState,
} from './types';

const initialState: NotificationState = {
  items: [],
};

const notificationReducer = (
  state: NotificationState = initialState,
  action: NotificationActionTypes
): NotificationState => {
  switch (action.type) {
    case ADD_NOTIFICATION:
      return {
        ...state,
        items: [
          ...state.items,
          {
            _id: action.payload.id,
            message: action.payload.message,
            date: Date.now(),
            read: false,
          },
        ],
      };
    case MARK_AS_READ:
      return {
        ...state,
        items: state.items.map((notification) =>
          notification._id === action.payload.id
            ? { ...notification, read: true }
            : notification
        ),
      };
    case MARK_ALL_AS_READ:
      return {
        ...state,
        items: state.items.map((notification) => ({
          ...notification,
          read: true,
        })),
      };
    case CLEAR_NOTIFICATION:
      return {
        ...state,
        items: [],
      };
    default:
      return state;
  }
};

export default notificationReducer;
