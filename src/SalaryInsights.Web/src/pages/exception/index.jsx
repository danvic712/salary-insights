import { Empty } from "@douyinfe/semi-ui";
import {
  IllustrationNotFound,
  IllustrationNotFoundDark,
} from "@douyinfe/semi-illustrations";
import { useRouteError } from "react-router-dom";

export default function Exception() {
  const error = useRouteError();
  console.error(error);

  const style = {
    width: 400,
    height: 400,
  };

  return (
    <Empty
      title={error.statusText}
      image={<IllustrationNotFound style={style} />}
      darkModeImage={<IllustrationNotFoundDark style={style} />}
      description={
        <div>
          <h1>Oops!</h1>
          <p>Sorry, Sorry, an unexpected error has occurred.</p>
          <p>
            <i>{error.message}</i>
          </p>
        </div>
      }
    ></Empty>
  );
}
