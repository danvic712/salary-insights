import { Layout, Typography } from '@douyinfe/semi-ui';
import { IconLink } from '@douyinfe/semi-icons';

export default function FooterComponent() {
    const { Footer } = Layout;
    const { Text } = Typography;

    let year = new Date().getFullYear();

    return (
        <Footer
            style={{
                display: 'flex',
                color: 'var(--semi-color-text-2)',
                fontSize: '14px',
                padding: '0px 20px',
                flexDirection: 'row-reverse',
                background: 'var(--semi-color-fill-0)',
            }}
        >
            <span>
                Copyright © {year}{' '}
                <Text
                    link={{
                        href: 'https://github.com/danvic712',
                        target: '_blank',
                    }}
                    icon={<IconLink />}
                >
                    Danvic Wang
                </Text>{' '}
                All Rights Reserved.{' '}
            </span>
        </Footer>
    );
}
