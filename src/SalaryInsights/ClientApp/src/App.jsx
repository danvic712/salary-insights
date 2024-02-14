import { Suspense } from 'react';

import RouteRenders from './routes/index';

function App() {
    return (
        <Suspense fallback={<div className="container">Loading...</div>}>
            <RouteRenders />
        </Suspense>
    );
}

export default App;
