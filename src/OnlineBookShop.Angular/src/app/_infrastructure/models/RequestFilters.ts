import { FilterLogicalOperators } from './FilterLogicalOperators';
import { Filter } from './Filter';

export interface RequestFilters {
    logicalOperator: FilterLogicalOperators;
    filters: Filter[];
}