-- https://www.hackerrank.com/challenges/the-pads/problem
select Name + '(' + substring(Occupation, 1, 1) + ')' as Col
from Occupations o
order by col asc;

select 
    'There are a total of ' + 
    convert(varchar(12), count(*)) + 
    ' ' + 
    lower(Occupation) +
    's.' as Col
from Occupations o
group by Occupation
order by count(*), Occupation;
