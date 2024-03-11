import { DetailsList, DetailsListLayoutMode, IColumn, Spinner, SpinnerSize } from '@fluentui/react';
import { ApiClient, ICurrencyDto } from 'app/generated/backend';
import React, { useEffect, useState } from 'react';

const ExchangeRates: React.FC = () => {
    const [data, setData] = useState({
        groups: [] as ICurrencyDto[],
        isFetching: false
    });

    const groupKeys: ICurrencyDto = {
        id: 0,
        currencyName: '',
        currencyCode: '',
        exchangeRate: 0,
    };

    const columns = Object.keys(groupKeys).map((key): IColumn => {
        return {
            key,
            name: key.replace(/([A-Z])/g, ' $1').replace(/^./, (str: string) => {
                return str.toUpperCase();
            }),
            fieldName: key,
            minWidth: 100,
            maxWidth: 200,
            isResizable: true
        };
    });

    useEffect(() => {
        const fetchData = async () => {
            try {
                setData({ groups: data.groups, isFetching: true });
                const result = await new ApiClient(process.env.REACT_APP_API_BASE).exchangeRate_GetListExchange();
                setData({ groups: result, isFetching: false });
            } catch (e) {
                console.log(e);
                setData({ groups: data.groups, isFetching: false });
            }
        };

        fetchData();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <>
            <h2>Exchange Rate</h2>
            <DetailsList
                items={data.groups.map((group) => {
                    return {
                        ...group,
                    };
                })}
                columns={columns}
                layoutMode={DetailsListLayoutMode.justified}
            />
            {data.isFetching && <Spinner size={SpinnerSize.large} />}
        </>
    );
};

export default ExchangeRates;
