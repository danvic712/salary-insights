import { Outlet } from 'react-router-dom';

import { Layout, BackTop } from '@douyinfe/semi-ui';

import Header from './header';
import Footer from './footer';

import * as styles from './index.scss';

export default function MainLayout() {
    const { Content } = Layout;

    return (
        <Layout>
            <Header />
            <Content
                style={{
                    backgroundColor: 'var(--semi-color-fill-0)',
                    height: '100vh',
                }}
            >
                <Outlet />
            </Content>
            <BackTop />
            <Footer />
        </Layout>
    );
}
